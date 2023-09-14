import { useFormikContext } from "formik";
import { FormControl as ChakraFormControl, FormErrorMessage, Input, InputGroup, InputRightElement, NumberDecrementStepper, NumberIncrementStepper, NumberInput, NumberInputField, NumberInputStepper, Select } from "@chakra-ui/react";
import { SingleDatepicker } from "chakra-dayzed-datepicker";
import { CalendarIcon } from "@chakra-ui/icons";
import { format } from "date-fns";
import getDayNames from "../lib/getDayNames"
import getMonthNames from "../lib/getMonthNames"
import { useTranslation } from "react-i18next";

export default function FormControl({ field, form, type, min, max, options, placeholder }) {
    const formik = useFormikContext()
    const { i18n } = useTranslation()

    return (
        <ChakraFormControl isDisabled={formik.isSubmitting} py={"4"} position={"relative"} isInvalid={formik.touched[field] && formik.errors[field]}>
            {
                {
                    'number': (<NumberInput min={min} max={max} form={form} type={type} {...formik.getFieldProps(field)} onChange={async value => formik.setFieldValue(field, value)}>
                        <NumberInputField />
                        <NumberInputStepper>
                            <NumberIncrementStepper />
                            <NumberDecrementStepper />
                        </NumberInputStepper>
                    </NumberInput>),
                    'select': (<Select placeholder={placeholder} form={form} {...formik.getFieldProps(field)}>
                        {options && options.map(option => <option key={option.value} value={option.value}>{option.label}</option>)}
                    </Select>),
                    'date': (
                        <InputGroup>
                            <SingleDatepicker
                                propsConfigs={{
                                    dayOfMonthBtnProps: {
                                        defaultBtnProps: {
                                            _hover: {
                                                background: 'gray.400',
                                            }
                                        },
                                        selectedBtnProps: {
                                            background: 'gray.200',
                                        },
                                        todayBtnProps: {
                                            borderWidth: '1px',
                                            borderColor: 'gray.200'
                                        }
                                    },
                                }}
                                configs={{
                                    monthNames: getMonthNames(i18n.language, 'short'),
                                    dayNames: getDayNames(i18n.language, 'short'),
                                    firstDayOfWeek: 1
                                }}
                                name={field}
                                date={new Date(formik.values[field])}
                                onDateChange={async date => await formik.setFieldValue(field, format(date, 'yyyy-MM-dd'))}
                            />
                            <InputRightElement>
                                <CalendarIcon />
                            </InputRightElement>
                        </InputGroup>
                    )
                }[type] || <Input form={form} type={type} {...formik.getFieldProps(field)} />
            }
            {formik.touched[field] && formik.errors[field] && <FormErrorMessage position={"absolute"} bottom={"-2.5"}>{formik.errors[field]}</FormErrorMessage>}
        </ChakraFormControl>
    )
}