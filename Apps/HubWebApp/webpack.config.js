const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const entry = {
    user: './Scripts/Internal/User/MainPage.ts',
    login: './Scripts/Hub/Auth/LoginPage.ts',
    home: './Scripts/Internal/Home/MainPage.ts',
    apps: './Scripts/Internal/Apps/MainPage.ts',
    appDashboard: './Scripts/Internal/AppDashboard/MainPage.ts',
    users: './Scripts/Internal/Users/MainPage.ts',
    appUser: './Scripts/Internal/AppUser/MainPage.ts'
};
const exportModule = {
    rules: [
        {
            test: /\.tsx?$/,
            use: 'ts-loader',
            exclude: /node_modules/
        },
        {
            test: /\.s[ac]ss$/i,
            use: [
                {
                    loader: 'file-loader',
                    options: {
                        name: '../../styles/css/[name].css',
                    },
                },
                'sass-loader',
            ]
        },
        {
            test: /\.css$/i,
            use: [
                {
                    loader: 'file-loader',
                    options: {
                        name: (resourcePath, resourceQuery) => {
                            if (/@fortawesome[\\\/]fontawesome-free/.test(resourcePath)) {
                                return '../../styles/css/fontawesome/[name].css';
                            }
                            return '../../styles/css/[name].css';
                        }
                    }
                }
            ]
        },
        {
            test: /\.html$/i,
            use: [{
                loader: 'html-loader',
                options: {
                    minimize: {
                        removeComments: false
                    },
                    esModule: false
                }
            }]
        },
        {
            test: /\.(svg|eot|woff|woff2|ttf)$/,
            use: [{
                loader: 'file-loader',
                options: {
                    name: '[name].[ext]',
                    outputPath: '../../styles/css/webfonts'
                }
            }]
        }
    ]
};
const outputFilename = '[name].js';

const resolve = {
    alias: {
        xtistart: path.resolve(__dirname, 'Scripts/Internal/Startup.js'),
        XtiShared: path.resolve(__dirname, 'Imports/Shared/')
    }
};
const plugins = [
    //new webpack.SourceMapDevToolPlugin({
    //    filename: "[file].map",
    //    fallbackModuleFilenameTemplate: '[absolute-resource-path]',
    //    moduleFilenameTemplate: '[absolute-resource-path]'
    //}),
    new MiniCssExtractPlugin({
        filename: '[name].css',
        chunkFilename: '[id].css',
    })
];
module.exports = [
    {
        mode: 'production',
        context: __dirname,
        devtool: false,
        entry: entry,
        module: exportModule,
        plugins: plugins,
        output: {
            filename: outputFilename,
            path: path.resolve(__dirname, 'wwwroot', 'js', 'dist')
        },
        resolve: resolve
    },
    {
        mode: 'development',
        context: __dirname,
        devtool: 'eval-source-map',
        entry: entry,
        module: exportModule,
        plugins: plugins,
        output: {
            filename: outputFilename,
            path: path.resolve(__dirname, 'wwwroot', 'js', 'dev')
        },
        resolve: resolve
    }
];