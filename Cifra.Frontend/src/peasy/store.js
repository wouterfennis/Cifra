/* eslint-disable import/prefer-default-export */
import { createStore, persist } from 'easy-peasy';

const translate = {
  more: 'More',
};
const store = createStore({
  translate: persist(translate),
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
