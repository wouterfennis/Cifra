/* eslint-disable import/prefer-default-export */
import { createStore, persist, action } from 'easy-peasy';

const translate = {
  more: 'More',
};
const ui = {
  navigationBar: {
    collapsed: false,
    toggleCollapsed: action((state) => {
      state.collapsed = !state.collapsed;
    }),
  },
};

const store = createStore({
  translate: persist(translate),
  ui,
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
