import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import { Provider } from "react-redux";
import { Reset } from "styled-reset";
import { createGlobalStyle } from "styled-components";
import store from './store'
import App from "./App";
import reportWebVitals from "./reportWebVitals";

const GlobalStyle = createGlobalStyle`
  *{
    box-sizing: border-box;
    font-family: 'Roboto', sans-serif !important;
  }
`;


ReactDOM.render(
  <React.StrictMode>
    <GlobalStyle />
    <Reset />
    <Provider store={store}>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </Provider>
  </React.StrictMode>,
  document.getElementById("root")
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
