from fastai.tabular.all import *

import logging
import sys

function_dir = os.path.dirname(os.path.realpath(__file__))
model_class_path = os.path.join(function_dir, '../data/20220919_model_win_loss')
model_reg_path = os.path.join(function_dir, '../data/20221015_model_reg')
rankings_csv_path = os.path.join(function_dir, '../data/20221023_rankings.csv')

model_class = load_learner(model_class_path)
model_reg = load_learner(model_reg_path)

def get_participant_rankings():
    return pd.read_csv(rankings_csv_path).set_index(['country_full'])

def predict_match_score(home_team, away_team):
        rankings = get_participant_rankings()

        row = pd.DataFrame(np.array([[]]))
        try:
            home_rank = rankings.loc[home_team, 'rank']
            away_rank = rankings.loc[away_team, 'rank']
        except:
            return null

        row['average_rank'] = (home_rank + away_rank) / 2
        row['rank_difference'] = home_rank - away_rank
        row['is_stake'] = True
        row['home_team'] = home_team
        row['away_team'] = away_team

        # classification
        dl_class = model_class.dls.test_dl(row, bs=1)
        preds_class, _ = model_class.get_preds(dl=dl_class)
        home_win_prob = preds_class.numpy()[0][1]
        logging.info('home win prob: {0}'.format(home_win_prob))

        if home_win_prob >= 0.5:
            row['is_won'] = True
        else:
            row['is_won'] = False

        # regression
        dl_reg = model_reg.dls.test_dl(row, bs=1)
        preds_reg, _ = model_reg.get_preds(dl=dl_reg)
        home_score_prob = preds_reg.numpy()[0][0]
        away_score_prob = preds_reg.numpy()[0][1]
        logging.info('regressor preds: {0}'.format(preds_reg))

        result = [math.floor(home_score_prob), math.floor(away_score_prob)]
        logging.info('score line: {0}'.format(result))
        
        return {'homeScore': math.floor(home_score_prob), 'awayScore': math.floor(away_score_prob)}

if __name__ == '__main__':
    predict_match_score(sys.argv[1])