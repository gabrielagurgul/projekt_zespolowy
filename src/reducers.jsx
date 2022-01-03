import { createSlice } from "@reduxjs/toolkit";

export const predictionSlice = createSlice({
  name: "prediction",
  initialState: {
    budgetType: [],
    availableCash: "",
    currentBudget: "",
    unavailableTypeCash: [],
  },
  reducers: {
    fetchBudgetType: (state, action) => {
      state.budgetType = action.payload;
    },
    fetchAvailableCash: (state, action) => {
      state.availableCash = action.payload;
    },
    fetchUnavailableTypeCash: (state, action) => {
      state.unavailableTypeCash = action.payload;
    },
    fetchCurrentBudget: (state, action) => {
      state.currentBudget = action.payload;
    },
  },
});

export const {
  fetchBudgetType,
  fetchAvailableCash,
  fetchUnavailableTypeCash,
  fetchCurrentBudget,
} = predictionSlice.actions;
export default predictionSlice.reducer;
