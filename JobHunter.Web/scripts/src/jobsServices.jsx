import axios from 'axios';

export function getAllJobs(){
    return axios.get("http://localhost:49647/api/jobs");
}
export function updateJob(data){
    return axios.put("http://localhost:49647/api/jobs/" + data.id, data);
}
export function addJob(data){
    return axios.post("http://localhost:49647/api/jobs", data);
}