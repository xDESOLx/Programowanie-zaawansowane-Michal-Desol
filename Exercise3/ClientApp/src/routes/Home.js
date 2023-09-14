import { Center, Wrap, WrapItem } from '@chakra-ui/react';
import React from 'react';
import NavCard from '../components/NavCard';
import { FaPeopleGroup, FaBook, FaBicycle, FaAddressBook } from "react-icons/fa6"
import { useTranslation } from 'react-i18next';

export default function Home() {
  const { t } = useTranslation()
  return (
    <Center height={'100%'}>
      <Wrap spacing={'5'} justify='center'>
        <WrapItem>
          <NavCard heading={t('Osoby')} icon={FaPeopleGroup} to="/people" />
        </WrapItem>
        <WrapItem>
          <NavCard heading={t('Książki')} icon={FaBook} to="/books" />
        </WrapItem>
        <WrapItem>
          <NavCard heading={t('Rowery')} icon={FaBicycle} to="/bikes" />
        </WrapItem>
        <WrapItem>
          <NavCard heading={t('Wypożyczenia książek')} icon={FaAddressBook} to="/book-rentals" />
        </WrapItem>
        <WrapItem>
          <NavCard heading={t('Wypożyczenia rowerów')} icon={FaAddressBook} to="/bike-rentals" />
        </WrapItem>
      </Wrap>
    </Center>
  )
}
