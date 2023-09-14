import React from 'react';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import { ChakraProvider } from '@chakra-ui/react';
import Home from './routes/Home';
import Root from './routes/Root';
import Error from './routes/Error';
import People from './routes/People';
import Books from './routes/Books';
import Bikes from './routes/Bikes';
import BikeRentals from './routes/BikeRentals';
import BookRentals from './routes/BookRentals';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <Root>
      <Error />
    </Root>,
    children: [
      {
        index: true,
        element: <Home />
      },
      {
        path: "/people",
        element: <People />
      },
      {
        path: "/books",
        element: <Books />
      },
      {
        path: "/bikes",
        element: <Bikes />
      },
      {
        path: "/book-rentals",
        element: <BookRentals />
      },
      {
        path: "/bike-rentals",
        element: <BikeRentals />
      }
    ]
  },
]);

export default function App() {
  return (
    <ChakraProvider>
      <RouterProvider router={router} />
    </ChakraProvider>
  )
}
