import { DeleteIcon, EditIcon, CheckIcon, CloseIcon } from "@chakra-ui/icons";
import { HStack, IconButton, Td, Text } from "@chakra-ui/react";
import { useTranslation } from "react-i18next";

export default function ActionsTableColumn({ mode, disabled, onEdit, onDelete, onConfirmEdit, onCancelEdit, onConfirmDelete, onCancelDelete }) {
    const { t } = useTranslation()

    if (mode === 'deleting') {
        return (
            <Td position={"relative"}>
                <Text top={"-0.5"} position={"absolute"} fontSize={"xs"} mb={"1"} fontWeight={"bold"}>{t('Czy na pewno?')}</Text>
                <HStack>
                    <IconButton isDisabled={disabled} onClick={onConfirmDelete} colorScheme={"green"} icon={<CheckIcon />} />
                    <IconButton isDisabled={disabled} onClick={onCancelDelete} colorScheme={"red"} icon={<CloseIcon />} />
                </HStack>
            </Td>
        )
    }

    if (mode === 'editing') {
        return (
            <Td>
                <HStack>
                    <IconButton isDisabled={disabled} onClick={onConfirmEdit} colorScheme={"green"} icon={<CheckIcon />} />
                    <IconButton isDisabled={disabled} onClick={onCancelEdit} colorScheme={"red"} icon={<CloseIcon />} />
                </HStack>
            </Td>
        )
    }

    return (
        <Td>
            <HStack>
                <IconButton isDisabled={disabled} onClick={onEdit} icon={<EditIcon />} />
                <IconButton isDisabled={disabled} onClick={onDelete} colorScheme={"red"} icon={<DeleteIcon />} />
            </HStack>
        </Td>
    )
}