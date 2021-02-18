module.exports = {
  env: {
    browser: true,
    es2021: true,
  },
  extends: [
    'plugin:react/recommended',
    'airbnb',
    'prettier'
  ],
  parserOptions: {
    ecmaFeatures: {
      jsx: true,
    },
    ecmaVersion: 12,
    sourceType: 'module',
  },
  plugins: [
    'react',
    'prettier'
  ],
  rules: {
    'linebreak-style': ['error', 'windows'],
    'react/prop-types': 0,

    'react/jsx-props-no-spreading': {
      html: 'ignore',
    },
    'react/jsx-filename-extension': [
      1,
      {
        extensions: ['.js', '.jsx'],
      }],
  },
};
