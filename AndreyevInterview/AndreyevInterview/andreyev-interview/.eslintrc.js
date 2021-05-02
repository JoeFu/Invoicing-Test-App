module.exports = {
    root: true,
    parserOptions: {
        parser: 'babel-eslint'
      },
    "env": {
        "browser": true,
        // "es6": true
    },
    "extends": "plugin:vue/essential",
    "globals": {
        "Atomics": "readonly",
        "SharedArrayBuffer": "readonly"
    },
    // "parserOptions": {
    //     "ecmaVersion": 2018,
    //     "sourceType": "module"
    // },
    "plugins": [
        "vue"
    ],
    "rules": {
          'generator-star-spacing': 'off',
           'no-debugger': process.env.NODE_ENV === 'production' ? 'error' : 'off'
    }
};