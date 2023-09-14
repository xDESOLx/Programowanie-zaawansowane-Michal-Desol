import { Card, CardBody, CardHeader, Heading, Icon } from "@chakra-ui/react";
import { NavLink } from "react-router-dom";

export default function NavCard({ heading, icon, to }) {
    return (
        <NavLink to={to}>
            <Card transition={'0.2s'} _hover={{ shadow: 'lg' }} width={'md'} align={'center'}>
                <CardHeader>
                    <Heading>{heading}</Heading>
                </CardHeader>
                <CardBody>
                    <Icon boxSize={'36'} as={icon} />
                </CardBody>
            </Card>
        </NavLink>
    )
}