import { Flex, Heading, Stack, Text } from "@chakra-ui/react";
import { useRouteError } from "react-router-dom";

export default function Error() {
    const error = useRouteError();
    return (
        <Flex align={"center"} justify={"center"} h={"100%"}>
            <Stack spacing={"5"} textAlign={"center"}>
                <Heading fontSize={"9xl"} fontWeight={"black"} as={"h1"}>{error.status}</Heading>
                <Text fontSize={"2xl"} fontWeight={"semibold"}>{error.statusText}</Text>
            </Stack>
        </Flex>
    )
}