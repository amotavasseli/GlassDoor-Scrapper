import React from 'react';
import {getAllJobs} from './jobsServices';
import CoverLetter from './CoverLetter';

class Jobs extends React.Component{
    state = {
        didMount: false
    }

    componentDidMount(){
        getAllJobs().then(
            response => {
                console.log(response.data);
                this.setState({
                    jobs: response.data,
                    didMount: true
                });
            },
            error => console.log(error)
        );
    }

    render(){
        return (
            <div className="container">
                {
                    this.state.didMount && this.state.jobs.map((job, index) => (
                        <div className="col-md-6 col-md-offset-3" key={index}>
                            <h3>Title: {job.Title} <span><CoverLetter jobdata={job} /></span></h3>
                            <h4>Company: {job.Company}</h4>
                            <h5>Location: {job.Location}</h5>
                            <a href={job.Link}>Link</a>
                            <h5>Post Date: {job.PostDate}</h5>
                            <p>Description: {job.Description}</p>
                        </div>
                    ))
                }
            </div>
        );
    }
}

export default Jobs; 