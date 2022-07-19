const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const ForkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');
const entry = {
    accessDenied: './Scripts/Internal/User/AccessDenied/MainPage.ts',
    error: './Scripts/Internal/User/Error/MainPage.ts',
    home: './Scripts/Internal/Home/MainPage.ts',
    apps: './Scripts/Internal/Apps/MainPage.ts',
    appDashboard: './Scripts/Internal/AppDashboard/MainPage.ts',
    userGroups: './Scripts/Internal/UserGroups/MainPage.ts',
    userQuery: './Scripts/Internal/UserQuery/MainPage.ts',
    users: './Scripts/Internal/Users/MainPage.ts',
    appUser: './Scripts/Internal/AppUser/MainPage.ts'
};
const exportModule = {
    rules: [
        {
            test: /\.tsx?$/,
            use: [
                {
                    loader: 'ts-loader',
                    // add transpileOnly option if you use ts-loader < 9.3.0
                    options: {
                        transpileOnly: true
                    }
                }
            ],
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
    extensions: [".ts", ".tsx", ".js"],
    alias: {
        '@jasonbenfield/sharedwebapp': 'C:/XTI/src/JasonBenfield/SharedWebApp/Apps/SharedWebApp/Scripts/Lib'
    }
};
const plugins = [
    new MiniCssExtractPlugin({
        filename: '[name].css',
        chunkFilename: '[id].css',
    }),
    new ForkTsCheckerWebpackPlugin()
];
module.exports = [
    {
        mode: 'production',
        context: __dirname,
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