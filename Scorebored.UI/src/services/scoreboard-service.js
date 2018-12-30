import axios from 'axios';
import {
  auth_header
} from '@/helpers/auth-header';

export const scoreboard_services = {
  get_group_leaderboard
};

function get_group_leaderboard(group_id) {
  return axios.get(`${process.env.VUE_APP_API_URL}/scoreboards/group_leaderboard/${group_id}`, {
    headers: auth_header()
  });
}
