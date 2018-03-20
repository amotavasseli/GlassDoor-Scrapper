import axios from 'axios';

export function getAllJobs(){
    return axios.get("http://localhost:49647/api/jobs");
}