import * as React from "react";
import { Routes, Route } from "react-router-dom";
import styled from "styled-components";
import ConfPage from "./pages/ConfPage";
import PredictionPage from "./pages/PredictionPage";

const Wrapper = styled.div`
  background: #eaf2e3;
  height: 100vh;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;

  
`;

const InnerWrapper = styled.div`
  box-sizing: border-box;
  background: #11163b;
  color: white;
  width: 80vw;
  height: 82vh;
  box-shadow: 0 0 1em black;
  position: relative;
  overflow: hidden;
  overflow-y: hidden;
  @media (max-width: 1064px) {
    overflow-y: scroll;
    padding-bottom: 1em;
  }
`;

function App() {
  return (
    <Wrapper>
      <InnerWrapper>
        <Routes>
          <Route path="/" element={<ConfPage />} />
          <Route path="prediction" element={<PredictionPage />} />
        </Routes>
      </InnerWrapper>
    </Wrapper>
  );
}

export default App;
