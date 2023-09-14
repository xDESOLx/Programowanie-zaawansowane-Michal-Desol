import React from 'react';
import { createRoot } from 'react-dom/client';
import './i18n';
import App from './App';

const rootElement = document.getElementById('root');
const root = createRoot(rootElement);

root.render(<App />);