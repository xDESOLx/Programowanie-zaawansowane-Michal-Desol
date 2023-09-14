import { Box, Button, Flex, HStack, Heading, Menu, MenuButton, MenuItem, MenuList, Text } from "@chakra-ui/react";
import NavLink from "../components/NavLink";
import { NavLink as RouterNavLink } from "react-router-dom";
import { Outlet } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { ChevronDownIcon } from "@chakra-ui/icons";

export default function Root({ children }) {
    const { t, i18n } = useTranslation()
    const intl = new Intl.DisplayNames([i18n.language], { type: "language" })
    return (
        <Flex direction={"column"} height={"100vh"}>
            <HStack spacing={"6"} as={"nav"} p={"4"} bg={"gray.50"} borderBottom={"1px"} borderBottomColor={"gray.100"}>
                <Heading as={RouterNavLink} to="/" size={"lg"} fontWeight={"extrabold"}>Exercise3</Heading>
                <HStack flexGrow={'1'} spacing={"2"}>
                    <NavLink to="/people">
                        {t('Osoby')}
                    </NavLink>
                    <NavLink to="/books">
                        {t('Książki')}
                    </NavLink>
                    <NavLink to="/bikes">
                        {t('Rowery')}
                    </NavLink>
                    <NavLink to="/book-rentals">
                        {t('Wypożyczenia książek')}
                    </NavLink>
                    <NavLink to="/bike-rentals">
                        {t('Wypożyczenia rowerów')}
                    </NavLink>
                </HStack>
                <Menu>
                    <MenuButton as={Button} rightIcon={<ChevronDownIcon />}>
                        {intl.of(i18n.language)}
                    </MenuButton>
                    <MenuList>
                        {i18n.options.supportedLngs
                            .filter(language => language !== 'cimode')
                            .map(language => <MenuItem onClick={async () => await i18n.changeLanguage(language)} key={language}>{intl.of(language)}</MenuItem>)}
                    </MenuList>
                </Menu>
            </HStack>
            <Box flexGrow={"1"} p={"10"}>
                {children ?? <Outlet />}
            </Box>
            <Box as={"footer"} p={"2"} bg={"gray.50"} borderTop={"1px"} borderTopColor={"gray.100"} textAlign={"center"}>
                <Text>Copyright &copy; 2023 Michał Desol</Text>
            </Box>
        </Flex>
    )
}