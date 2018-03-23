import React from 'react';
import {getAllJobs, updateJob} from './jobsServices';
import CoverLetter from './CoverLetter';
import JobForm from './JobForm';

class Jobs extends React.Component{
    state = {
        activeList: null,
        showJobForm: false
    }

    archivedJobs = [];
    appliedJobs = [];
    remainingJobs = [];

    componentDidMount(){
        getAllJobs().then(
            response => {
                console.log(response.data);
                const allJobs = response.data; 
                for(let i = 0; i < allJobs.length; i++){
                    if(allJobs[i].Archived) {
                        this.archivedJobs.push(allJobs[i]);
                    } else if(!allJobs[i].Archived && allJobs[i].DateApplied != null) {
                        this.appliedJobs.push(allJobs[i]);
                    } else {
                        this.remainingJobs.push(allJobs[i]);
                    }
                }
            },
            error => console.log(error)
        );
    }
    
    addJob = () => {
        this.setState({showJobForm: true});
    }
    onApplied = event => {
        console.log(event.target.checked);
        if(event.target.checked){
            const date = new Date();
            event.target.value = date.toDateString();
        }
    }
    didApply = date => {
        const convert = new Date(date);
        return convert.toDateString();
    }

    remaining = () => {
        this.setState({
            activeList: this.remainingJobs,
            showJobForm: false
        });
        console.log(this.remainingJobs);
    }
    applied = () => {
        this.setState({
            activeList: this.appliedJobs,
            showJobForm: false
        });
        console.log(this.appliedJobs);
    }
    archived = () => {
        this.setState({
            activeList: this.archivedJobs,
            showJobForm: false
        });
        console.log(this.archivedJobs);
    }
    onApply = job => {
        const date = new Date();
        job.DateApplied =  date.toDateString();
        updateJob(job).then(
            response => {
                this.setState({
                    activeList: this.remainingJobs
                });
            }
        )
    }
    onArchive = job => {
        job.Archived = true; 
        updateJob(job).then(
            response => {
                console.log(response);
                this.setState({
                    activeList: this.remainingJobs
                });
            }
        )
    }
    

    render(){
        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-4">
                        <button 
                            type="button" 
                            className="btn btn-primary" 
                            onClick={() => this.remaining()}
                        >
                            Jobs
                        </button>
                        <button
                            type="button" 
                            className="btn btn-success" 
                            onClick={() => this.applied()}
                        >
                            Applied Jobs
                        </button>
                        <button 
                            type="button" 
                            className="btn btn-warning" 
                            onClick={() => this.archived()}
                        >
                            Archived Jobs
                        </button>
                        <button 
                            type="button" 
                            className="btn btn-info" 
                            onClick={() => this.addJob()}
                        >
                            Add Jobs
                        </button>
                        <br />
                        <br />
                        <br />
                    </div>
                    <div className="col-md-8">
                        {
                            this.state.showJobForm ? <JobForm /> :
                            this.state.activeList && this.state.activeList.map((job, index) => (
                                <div className="col-md-6 col-md-offset-2" key={index}>
                                    <h3>Title: {job.Title} <span><CoverLetter jobdata={job} /> </span></h3>
                                    {
                                        job.DateApplied ? 
                                            <h5 style={{color: "green"}}>
                                                Applied on {this.didApply(job.DateApplied)}
                                            </h5> 
                                            : <h5>
                                            </h5>
                                    }
                                    <h4>Company: {job.Company}</h4>
                                    <h5>Location: {job.Location}</h5>
                                    <a href={job.Link}>Link</a>
                                    <h5>Post Date: {job.PostDate}</h5>
                                    <p>Description: {job.Description}</p>
                                    {
                                        job.DateApplied || job.Archived ? <div></div> : 
                                            <div>
                                                <button 
                                                    type="button"
                                                    className="btn-success" 
                                                    onClick={() => this.onApply(job)}
                                                >
                                                    Applied
                                                </button>
                                                <button 
                                                    type="button"
                                                    className="btn-warning" 
                                                    onClick={() => this.onArchive(job)}
                                                >
                                                    Archive
                                                </button>
                                            </div>
                                    }
                                </div>
                            ))
                        }
                    </div>
                </div>
            </div>
        );
    }
}

export default Jobs; 