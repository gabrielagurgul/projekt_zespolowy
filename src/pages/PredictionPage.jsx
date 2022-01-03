import React, { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import {
  fetchBudgetType,
  fetchAvailableCash,
  fetchUnavailableTypeCash,
  fetchCurrentBudget,
} from "../reducers";
import PredictionTemp from "../templates/PredictionTemp";

const PredictionPage = () => {
  const { budgetType, availableCash, unavailableTypeCash, currentBudget } =
    useSelector((state) => state.prediction);
  const dispatch = useDispatch();

  useEffect(() => {
    fetch("/api/budgetType")
      .then((res) => {console.log(res)
        return res.json()})
      .then((data) => dispatch(fetchBudgetType(data)));

    fetch("/api/lastSebastian")
      .then((res) => res.json())
      .then((data) => dispatch(fetchCurrentBudget(data)));

    refreshBudgetType();
  }, []);

  const refreshBudgetType = () => {
    fetch("/api/availableCash")
      .then((res) => res.json())
      .then((data) => dispatch(fetchAvailableCash(data)));

    fetch("/api/unavailableTypeCash")
      .then((res) => res.json())
      .then((data) => dispatch(fetchUnavailableTypeCash(data)));
  };
  return (
    <>
      <PredictionTemp
        budgetType={budgetType}
        availableCash={availableCash}
        currentBudget={currentBudget}
        unavailableTypeCash={unavailableTypeCash}
        refreshBudgetType={refreshBudgetType}
      />
    </>
  );
};

export default PredictionPage;
