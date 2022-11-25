const path = require('path');

module.exports = {
  entry: './src/js/main.js',
  output: {
    clean: true,
    filename: '[name].entry.js',
    path: path.resolve(__dirname, '../', 'wwwroot', 'dist'),
  },
  mode: 'development',
  devtool: 'source-map',
  module: {
    rules: [
      {
        test: /\.css$/,
        use: [
          'style-loader',
          'css-loader',
        ],
      },
      {
        test: /\.(eot|woff(2)?|tff|otf|svg)$/i,
        type: 'asset'
      },
    ],
  },
};
