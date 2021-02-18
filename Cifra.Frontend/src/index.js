import React from "react";
import ReactDOM from "react-dom";
import './global/theme.scss'
// import "service-worker"
import App from "./containers/App";
import { StoreProvider } from "easy-peasy";
import { store } from "./peasy/store";

const wrapper = document.getElementById("container");
wrapper ? ReactDOM.render(
  <StoreProvider store={store}>
    <App />
  </StoreProvider>
  , wrapper) : false;


console.log('serviceWorker??', 'serviceWorker' in navigator)
if ('serviceWorker' in navigator) {
  console.log('serviceWorker')

  window.addEventListener('load', () => {
    navigator.serviceWorker.register('/service-worker.js').then(registration => {
      console.log('SW registered: ', registration);
    }).catch(registrationError => {
      console.log('SW registration failed: ', registrationError);
    });
  });
}