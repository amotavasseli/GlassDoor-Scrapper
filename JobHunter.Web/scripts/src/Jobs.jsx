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
    didApply = event => {
        console.log(event.target.checked);
        if(event.target.checked){
            event.target.value = new Date();
        }
        console.log(event.target.value.toDateString());
        //event.target.value = !event.target.value;
        
    }

    render(){
        return (
            <div className="container">
                {
                    this.state.didMount && this.state.jobs.map((job, index) => (
                        <div className="col-md-6 col-md-offset-2" key={index}>
                            <h3>Title: {job.Title} <span><CoverLetter jobdata={job} /> </span></h3>
                         
                            <h5>Applied?
                                <input type="checkbox" value={job.DateApplied ? true : false} checked={this.state.checked} onChange={this.didApply} />
                            </h5>
                            
                            {
                                job.DateApplied ? <h5 style={{color: "green"}}>job.DateApplied</h5> : <div></div>
                            }
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