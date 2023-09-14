module.exports = {
    webpack: {
        configure: (webpackConfig, { env, paths }) => {
            webpackConfig.experiments = {
                topLevelAwait: true
            }
            return webpackConfig;
        },
    },
};