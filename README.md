# wikentipp
Web application for the football world cup 2022 where users can compete against each other and against the **BIG BRAIN AI**.

## Project structure
    .
    ├── AzureFunction                   # The azure-function for the predictive model to predict the scoreline of two worldcup teams
    ├── Predictor                       # Everything involving the modeling of the predictor
    │   ├── datasets                    # The datasets used for analysis, training and inference
    |   ├── modeling                    # Jupyter notebooks for data evaluation, training, testing and inference tests
    |   └── models                      # The trained models
    ├── WebApp                          # The .net Core 6.0 with Angular web application
    └── README.md