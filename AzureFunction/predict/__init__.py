import logging
import azure.functions as func
import json

# Import helper script
from .predict import predict_match_score

def main(req: func.HttpRequest) -> func.HttpResponse:
    home_team = req.params.get('homeTeam')
    away_team = req.params.get('awayTeam')
    logging.info('Home team: ' + home_team)
    logging.info('Away team: ' + away_team)

    results = predict_match_score(home_team, away_team)

    if not results:
        return func.HttpResponse("Prediction failed because of wrong input", status_code=400)

    headers = {
        "Content-type": "application/json",
        "Access-Control-Allow-Origin": "*"
    }

    return func.HttpResponse(json.dumps(results), headers = headers)