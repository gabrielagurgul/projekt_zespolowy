import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate, Link } from "react-router-dom";
import styled from "styled-components";
import bgImage1 from "../images/p1.png";
import bgImage2 from "../images/p2.png";
import bgImage3 from "../images/test.png";

const Navbar = styled.nav`
  padding: 0.5rem 0 0.5rem 2rem;
  width: 100%;
  background: #191f4b;
`;

const BgImage1Styled = styled.img`
  width: 200px;
  border: none;
  border-bottom: none !important;
  outline: none !important;
  border-image: none;
  height: 200px;
  background-size: contain;
  overflow: hidden;
  position: absolute;
  right: 0;
  bottom: 0;
`;

const BgImage2Styled = styled.img`
  width: 285px;
  border: none;
  border-bottom: none !important;
  outline: none !important;
  border-image: none;
  height: 220px;
  background-size: contain;
  overflow: hidden;
  position: absolute;
  left: -3rem;
  top: 2rem;
`;

const BgImage3Styled = styled.img`
  width: 400px;
  border: none;
  border-bottom: none !important;
  outline: none !important;
  border-image: none;
  height: 265px;
  background-size: contain;
  overflow: hidden;
  position: absolute;
  left: 3rem;
  bottom: -1.1rem;
`;

const FormWrapper = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  margin-top: 5rem;
  align-items: center;
  background: #11163b;
  height: 100%;
`;

const FormWrapperInner = styled.form`
  display: flex;
  flex-direction: column;
  position: relative;
`;

const LabelStyled = styled.label`
  align-self: flex-start;
  padding: 0.3rem 0;
  margin-top: 2rem;
`;

const InputStyled = styled.input`
  padding: 0.7rem 0.2rem;
  background-color: #191f4b;
  border: 2px solid #292e57;
  border-radius: 3px;
  width: 400px;
  color: #fff;
  font-size: 1em;

  &:focus {
    outline: none;
    border-color: #7c7e8d;
  }
`;

const FormTitleStyled = styled.h1`
  text-align: center;
  font-size: 1.5em;
`;

const BrStyled = styled.hr`
  margin-top: 2rem;
  width: 10em;
  border: 2px solid #292e57;
`;

const FormBtnStyled = styled.button`
  margin-top: 2rem;
  padding: 1.4rem;
  background-color: #ec6484;
  border: 2px solid #e97792;
  border-radius: 3px;
  font-size: 1.2em;
  font-weight: 400;
  color: #fff;

  &:hover {
    cursor: pointer;
    background-color: #eb5276;
    border: 2px solid #ec6484;
  }
`;
const ErrorFlagStyled = styled.div`
  text-align: center;
  background-color: rgba(233, 119, 146, 0.5);
  border: 1px solid #ec6484;
  border-radius: 3px;
  padding: 1rem 0;
  margin-top: 1rem;
`;

const ConfTemp = () => {
  let navigate = useNavigate();
  const [error, setError] = useState("");
  const { register, handleSubmit } = useForm();

  const onSubmit = (e) => {
    if (e.balance < 0 || e.balance === "" || e.solary < 0 || e.solary === "") {
      setError("Wprowadz poprawne wartości w polach formularza!");
    } else {
      setError("");
      fetch(`/api/budget/add/10`, {
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        method: "POST",
        body: JSON.stringify({
          description: "",
          amount: e.solary,
          addedData: getCurrentDateTime(),
        }),
      })
        .then((res) => res.status)
        .then((data) => {
          if (data === 201 && e.balance === "") navigate("/prediction");
        });

      fetch(`/api/budget/add/11`, {
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        method: "POST",
        body: JSON.stringify({
          description: "",
          amount: e.balance,
          addedData: getCurrentDateTime(),
        }),
      })
        .then((res) => res.json())
        .then(() => navigate("/prediction"));
    }
  };

  const getCurrentDateTime = () => {
    const today = new Date();
    const currerntDate =
      today.getFullYear() +
      "-" +
      (today.getMonth() + 1) +
      "-" +
      today.getDate();
    var time =
      (today.getHours() < 10 ? "0" : "") +
      today.getHours() +
      ":" +
      (today.getMinutes() < 10 ? "0" : "") +
      today.getMinutes() +
      ":" +
      (today.getSeconds() < 10 ? "0" : "") +
      today.getSeconds();

    return currerntDate + "T" + time;
  };

  return (
    <>
      <BgImage1Styled src={bgImage1} />
      <BgImage2Styled src={bgImage2} />
      <BgImage3Styled src={bgImage3} />
      <Navbar>Strona konfiguracyjna</Navbar>
      <FormWrapper>
        <FormWrapperInner onSubmit={handleSubmit(onSubmit)}>
          <FormTitleStyled>Skonfiguruj swój budżet</FormTitleStyled>
          <BrStyled />
          {error ? <ErrorFlagStyled>{error}</ErrorFlagStyled> : ""}
          <LabelStyled>Saldo</LabelStyled>
          <InputStyled {...register("balance")} type="number" />
          <LabelStyled>Miesięczne wynagrodzenie</LabelStyled>
          <InputStyled {...register("solary")} type="number" />
          <FormBtnStyled>Zatwierdzam</FormBtnStyled>
          <Link
            to="/prediction"
            style={{
              alignSelf: "flex-end",
              color: "#e94a6f",
              marginTop: "1em",
              textDecoration: "none",
            }}
          >
            Pomiń &#10140;
          </Link>
        </FormWrapperInner>
      </FormWrapper>
    </>
  );
};

export default ConfTemp;
