const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const entry = {
    user: './Imports/Shared/User/UserPage.js',
    home: './Scripts/Internal/Home/MainPage.ts',
    userAdmin: './Scripts/Internal/UserAdmin/MainPage.ts',
    apps: './Scripts/Internal/Apps/MainPage.ts',
    appDashboard: './Scripts/Internal/AppDashboard/MainPage.ts',
    users: './Scripts/Internal/Users/MainPage.ts'
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
                    }
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
        XtiShared: path.resolve(__dirname, 'Imports/Shared/'),
        XtiAuthenticator: path.resolve(__dirname, 'Imports/Authenticator/')
    }
};
const plugins = [
    new MiniCssExtractPlugin({
        filename: '[name].css',
        chunkFilename: '[id].css',
    })
];
module.exports = [
    {
        mode: 'production',
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