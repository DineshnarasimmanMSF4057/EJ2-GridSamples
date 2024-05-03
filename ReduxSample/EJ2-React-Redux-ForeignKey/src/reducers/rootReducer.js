import { combineReducers } from 'redux'
import dynamicFilter from './dynamicgridReducer';

const todoApp = combineReducers({
  dynamicFilter
})

export default todoApp
