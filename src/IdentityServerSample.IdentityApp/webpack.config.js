﻿const path = require('path');

module.exports = {
  entry: './wwwroot/src/main.js',
  output: {
    path: path.resolve(__dirname, 'wwwroot', 'dist'),
    filename: '[name].js',
  },
  mode: 'development'
};
