import { Box } from "@chakra-ui/react";
import { NavLink as RouterNavLink, useMatch } from "react-router-dom";

export default function NavLink({ children, to }) {
    const active = useMatch(to)

    return (
        <Box bg={active ? 'gray.100' : ''} fontWeight={"medium"} px={"2"} py={"1"} rounded={"lg"} _hover={{ bg: 'gray.200' }} as={RouterNavLink} to={to}>
            {children}
        </Box>
    )
}