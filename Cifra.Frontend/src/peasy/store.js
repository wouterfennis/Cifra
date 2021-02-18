/* eslint-disable import/prefer-default-export */
import { action, createStore, persist, computed, thunk} from 'easy-peasy';
import { formatBytes } from 'global/functions';
import {find, findIndex} from "lodash";

const ui = {
  control: {
  }
}

const translate = {
  more: 'More'
}

const content = {
}
const store = createStore({
  translate,
  ui,
  content: persist(content)
});

// Wrapping dev only code like this normally gets stripped out by bundlers
// such as Webpack when creating a production build.
if (process.env.NODE_ENV === 'development') {
  if (module.hot) {
    module.hot.accept('./model', () => {
      store.reconfigure(model); // 👈 Here is the magic
    });
  }
}

export { store };
