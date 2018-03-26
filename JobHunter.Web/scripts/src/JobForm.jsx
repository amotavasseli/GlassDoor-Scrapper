import React from 'react';
import {addJob} from './jobsServices';


class JobForm extends React.Component {
    state = {
        inputs: {}
    }

    addJobs = () => {
        addJob(this.state.inputs).then(
            response => console.log(response),
            error => console.log(error)
        );
    }
    handleChange = (input, e) => {
        this.setState({
            inputs: {
                ...this.state.inputs,
                [input]: e.target.value
            }
        })
    }


    render(){
        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-6">
                        <form>
                            <div className="form-group">
                                <label>Title</label>
                                <input type="text" className="form-control" onChange={e => this.handleChange("title", e)} />
                            </div>
                            <div className="form-group">
                                <label>Company</label>
                                <input type="text" className="form-control" onChange={e => this.handleChange("company", e)} />
                            </div>
                            <div className="form-group">
                                <label>Description</label>
                                <textarea type="text" className="form-control" onChange={e => this.handleChange("description", e)} />
                            </div>
                            <div className="form-group">
                                <label>Link</label>
                                <input type="text" className="form-control" onChange={e => this.handleChange("link", e)} />
                            </div>
                            <div className="form-group">
                                <label>Location</label>
                                <input type="text" className="form-control" placeholder="city, state" onChange={e => this.handleChange("location", e)} />
                            </div>
                            <div className="form-group">
                                <label>Post Date</label>
                                <input type="text" className="form-control" placeholder="MM/DD/YYYY" onChange={e => this.handleChange("postDate", e)} />
                            </div>
                            <div className="form-group">
                                <label>Date Applied</label>
                                <input type="text" className="form-control" placeholder="MM/DD/YYYY" onChange={e => this.handleChange("dateApplied", e)} />
                            </div>
                            <button type="button" className="btn btn-primary pull-right" onClick={() => this.addJobs()}>Add</button>
                        </form>
                    </div>
                </div>
            </div>
        )
    }
}

export default JobForm;