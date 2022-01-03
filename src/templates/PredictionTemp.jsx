import React, { useState } from "react";
import { useForm } from "react-hook-form";
import styled from "styled-components";
import { Chart as ChartJS, ArcElement, Tooltip, Legend, Title } from "chart.js";
import { Doughnut } from "react-chartjs-2";
import bgImage1 from "../images/p1.png";
import bgImage2 from "../images/p2.png";
import Food from "../images/food.png";
import Car from "../images/car.png";
import Entertainment from "../images/entertainment.png";
import Family from "../images/family.png";
import Health from "../images/healthcare.png";
import Stimulant from "../images/stimulant.png";
import Home from "../images/house.png";
import Investment_S from "../images/investment.png";
import Investment_R from "../images/self-regulation.png";
import Rich from "../images/rich.png";
import Budget from "../images/budget.png";

ChartJS.register(ArcElement, Tooltip, Legend, Title);

const images = {
  1: Food,
  2: Home,
  3: Health,
  4: Family,
  5: Car,
  6: Entertainment,
  7: Stimulant,
  8: Investment_S,
  9: Investment_R,
  10: Rich,
  11: Budget,
};

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
  margin: 0 auto;
  margin-top: 0;
  max-width: 43vw;
  @media (max-width: 1485px) {
    max-width: 45vw;
  }
  @media (max-width: 1419px) {
    max-width: 60vw;
  }
`;

const AppCeneterd = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 93%;
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
  left: -6rem;
  top: 2rem;
`;

const Navbar = styled.nav`
  padding: 0.5rem 2rem 0.5rem 2rem;
  width: 100%;
  background: #191f4b;
  position: relative;
  z-index: 9999;
  display: flex;
  justify-content: space-between;
`;

const DoughnutStyle = styled.div`
  position: relative;
  margin-right: 0.8em;
  background: #11163b;
`;

const PreventClick = styled.div`
  width: 205px;
  &:last-child:active {
    pointer-events: none;
  }
`;

const DoughnutWrapper = styled.div`
  display: flex;
  height: 100px;
  align-items: center;
  position: relative;
  z-index: 999;
`;
const ImgStyled = styled.img`
  width: 35px;
  border: none;
  border-bottom: none !important;
  outline: none !important;
  border-image: none;
  height: 35px;
  background-size: contain;
  overflow: hidden;
  position: absolute;
  z-index: -1;
  left: 34px;
  top: 49px;
`;

const FlexRowStyled = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: center;
  position: relative;
`;

const FlexColumnStyled = styled.div`
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  margin: 0.2rem;
`;

const CeneterContainer = styled.div`
  margin: auto;
`;
const FormWrapper = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  margin-top: 2rem;
  align-items: center;
  background: #11163b;
`;

const FormWrapperInner = styled.form`
  display: flex;
  flex-direction: column;
  position: relative;
`;

const LabelStyled = styled.label`
  align-self: flex-start;
  padding: 0.3rem 0;
  margin-top: 1rem;
`;

const InputStyled = styled.input`
  padding: 0.4rem 0.2rem;
  background-color: #191f4b;
  border: 2px solid #292e57;
  border-radius: 3px;
  width: 350px;
  color: #fff;
  font-size: 1em;

  &:focus {
    outline: none;
    border-color: #7c7e8d;
  }
  &.predykt {
    margin: auto;
    width: 300px;
    text-align: center;
  }
  &.type {
    width: 250px;
  }
`;

const FormTitleStyled = styled.h1`
  margin: 1rem;
  text-align: center;
  font-size: 1.5em;
`;

const BrStyled = styled.hr`
  margin-top: 1rem;
  width: 10em;
  border: 2px solid #292e57;

  &.isError {
    margin-top: 0.2rem;
  }
`;

const FormBtnStyled = styled.button`
  align-self: center;
  margin-top: 0.5rem;
  padding: 0.6rem;
  background-color: #347c8b;
  border: 2px solid #4695a5;
  border-radius: 3px;
  font-size: 1.2em;
  font-weight: 400;
  color: #fff;
  margin-left: 0.2rem;

  &:hover {
    cursor: pointer;
    background-color: #4695a5;
    border: 2px solid #347c8b;
  }

  &.prdykt {
    background-color: #2769a3;
    border: 2px solid #397ebb;

    &:hover {
      cursor: pointer;
      background-color: #397ebb;
      border: 2px solid #2769a3;
    }
  }
`;
const ErrorFlagStyled = styled.div`
  text-align: center;
  background-color: rgba(233, 119, 146, 0.5);
  border: 1px solid #ec6484;
  border-radius: 3px;
  padding: 0.3rem 0;

  &.success {
    background-color: rgba(52, 124, 139, 0.8);
    border: 1px solid #347c8b;
  }
`;

const PredictionTemp = ({
  budgetType,
  availableCash,
  currentBudget,
  unavailableTypeCash,
  refreshBudgetType,
}) => {
  const [state, setState] = useState("");
  const [predict, setPredict] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const { register, handleSubmit, reset } = useForm();

  const handleClick = (event, id) => {
    setState(budgetType[id - 1]);
  };

  const DoughnutRenderer = (id, name) => {
    const data = {
      labels: ["Wydatek", "Budżet"],
      datasets: [
        {
          data: [unavailableTypeCash[id - 1], currentBudget],
          backgroundColor: [
            "rgba(255, 99, 132, 0.6)",
            // "rgba(54, 162, 235, 0.6)",
            "rgba(75, 192, 192, 0.6)",
          ],
          borderColor: [
            "rgba(255, 99, 132, 1)",
            // "rgba(54, 162, 235, 1)",
            "rgba(75, 192, 192, 1)",
          ],
          borderWidth: 1,
        },
      ],
    };

    const options = {
      plugins: {
        legend: {
          display: true,
          position: "right",
        },
      },
    };

    return (
      <DoughnutStyle key={id}>
        <div style={{ position: "relative", zIndex: "10" }}>
          <span
            style={{
              position: "relative",
              zIndex: "-999",
              top: "25px",
              right: "-108px",
            }}
          >
            {name}
          </span>
          <DoughnutWrapper onClick={(e) => handleClick(e, id)}>
            <PreventClick>
              <Doughnut data={data} options={options} width={200} height={50} />
            </PreventClick>
          </DoughnutWrapper>
          <ImgStyled src={images[id]} />
        </div>
      </DoughnutStyle>
    );
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
  const onSubmitPredict = (e) => {
    if (e.cost < 0 || e.cost === "" || e.desc.length < 3 || state === "") {
      setError(
        "Wprowadz poprawne wartości w polach formularza! Kotwa musi być dodatnia,  Opis min. 3 znaki"
      );
    } else {
      const data = {
        description: e.desc,
        amount: e.cost,
        addedData: getCurrentDateTime(),
      };
      fetch(`/api/prediction/${state.id}`, {
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        method: "PUT",
        body: JSON.stringify(data),
      })
        .then((res) => res.json())
        .then((data) => {
          setError("");
          setPredict(data);
        });
    }
  };

  const onSubmit = (e) => {
    if (e.cost < 0 || e.cost === "" || e.desc.length < 3 || state === "") {
      setError(
        "Wprowadz poprawne wartości w polach formularza! Kotwa musi być dodatnia,  Opis min. 3 znaki"
      );
    } else {
      const data = {
        description: e.desc,
        amount: e.cost,
        addedData: getCurrentDateTime(),
      };

      fetch(`/api/budget/add/${state.id}`, {
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        method: "POST",
        body: JSON.stringify(data),
      })
        .then((res) => res.status)
        .then((data) => {
          if (data === 201) {
            setError("");
            setSuccess("Wydatek został dodany prawidłowo");
            refreshBudgetType();
            reset();
            setPredict("");
            setTimeout(() => {
              setSuccess("");
            }, 2000);
          }
        });
    }
  };

  return (
    <>
      <BgImage1Styled src={bgImage1} />
      <BgImage2Styled src={bgImage2} />
      <Navbar>
        Analizer Budżetu
        <div>
          Wpłata: {currentBudget} zł <span style={{ marginLeft: "1em" }}></span>{" "}
          Saldo: {availableCash} zł
        </div>
      </Navbar>
      <AppCeneterd>
        <Wrapper>
          <FlexRowStyled>
            {budgetType.map((type) => {
              if (type.id < 10) return DoughnutRenderer(type.id, type.type);
            })}
          </FlexRowStyled>
        </Wrapper>
        <FormWrapper>
          <FormWrapperInner>
            <CeneterContainer>
              <FormTitleStyled>
                Predykuj dla klasyfikatora {state.type ? state.type : "Brak"}
              </FormTitleStyled>
              <FlexColumnStyled>
                <InputStyled
                  className="predykt"
                  disabled
                  type="number"
                  value={predict}
                />
              </FlexColumnStyled>
            </CeneterContainer>
            <BrStyled className={error ? "isError" : ""} />
            {error ? <ErrorFlagStyled>{error}</ErrorFlagStyled> : ""}
            {success ? (
              <ErrorFlagStyled className="success">{success}</ErrorFlagStyled>
            ) : (
              ""
            )}
            <FlexRowStyled>
              <FlexColumnStyled>
                <LabelStyled>Kwota</LabelStyled>
                <InputStyled {...register("cost")} type="number" />
              </FlexColumnStyled>
              <FlexColumnStyled>
                <LabelStyled>Opis</LabelStyled>
                <InputStyled {...register("desc")} type="text" />
              </FlexColumnStyled>
            </FlexRowStyled>
            <FlexRowStyled>
              <CeneterContainer>
                <FormBtnStyled
                  className="prdykt"
                  onClick={handleSubmit(onSubmitPredict)}
                >
                  Predykuję
                </FormBtnStyled>
                <FormBtnStyled onClick={handleSubmit(onSubmit)}>
                  Zatwierdzam
                </FormBtnStyled>
              </CeneterContainer>
            </FlexRowStyled>
          </FormWrapperInner>
        </FormWrapper>
      </AppCeneterd>
    </>
  );
};

export default PredictionTemp;
