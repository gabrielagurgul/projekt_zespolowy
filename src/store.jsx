import { configureStore } from "@reduxjs/toolkit";
import predictionReducer from "./reducers";

export default configureStore({
  reducer: {
    prediction: predictionReducer,
  },
});
