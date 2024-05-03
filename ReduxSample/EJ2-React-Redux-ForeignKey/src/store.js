import { createStore } from 'redux';
import reducer from './reducers/rootReducer'

export default function Store() {
  return createStore(
    reducer
  );
}
