import { data, customerData } from "../components/data";
const gridData = { data, customerData };
const dynamicFilter = (state = gridData, action) => {
  switch (action.type) {
    case "CustomAction":
      //custom functions
      return state 
    default:
      //returning data to the grid
      return state;
  }
};

export default dynamicFilter;
