import React from 'react';
import {Modal} from 'react-bootstrap';
import jsPDF from 'jspdf'; 


class CoverLetter extends React.Component{
    state = {
        modalIsOpen: false

    }
    changeModalState = () => {
        let currentState = !this.state.modalIsOpen;
        this.setState({modalIsOpen: currentState});
    }
    downloadPDF = () => {
        const pdf = new jsPDF('p', 'cm', "a4");
        let doc = document.getElementById("cover-letter");
        pdf.fromHTML(
            doc,
            1.27,
            1.27, 
            {
                'width': 17.78
            },
        );
        
        let company = this.props.jobdata.Company; 
        company = company.replace(/\s+/g, "");
        pdf.save(company + "CoverLetter.pdf");
        
    }


    

    render(){
        return(
            <span>
                <button type="button" className="btn btn-info" onClick={this.changeModalState}>CL</button>
                <Modal show={this.state.modalIsOpen} onHide={this.changeModalState}>
                    <Modal.Header closeButton></Modal.Header>
                    <Modal.Body>
                        {
                            this.state.modalIsOpen ? 
                            <div id="cover-letter" contentEditable="true">
                                
                                <p>Dear <span>{this.props.jobdata.Company}</span> Team,</p>
                                <p>I am writing to express my interest in your <span>{this.props.jobdata.Title}</span> position in <span>{this.props.jobdata.Location}</span>. I have a solid reputation for providing high quality solutions to address business needs, and I am seeking to join a company that can utilize and benefit from my unique background and experience.</p>
                                <p>Recently, as a Full Stack Web Developer at Babel Dabble, a real-time competitive calligraphy platform and learning management system for educators and students, I have gained significant experience that makes me an ideal addition to your company. My experience includes the following languages, libraries, and frameworks: Javascript, jQuery, AngularJS, React, C#, ASP.NET, and T-SQL.</p>
                                <p>In addition to my knowledge base and experience, I am known for being resourceful, innovative, and for actively seeking out new technologies and staying up-to-date on industry trends and technological advancements. I am confident that my abilities to produce excellent code and to clearly communicate and collaborate with management, co-workers and customers will lead to great success with your company. I believe that my qualifications make me an ideal candidate, and that the members of your team would very much enjoy meeting and working with me.</p>
                                <p>My resume is attached for your review and consideration. Please contact me if you would like to arrange an interview, or if you have any questions with regard to my application.</p>
                                <p style={{marginTop: "4em"}}>Sincerely,</p>
                                <p>Arian Motavasseli</p>
                                
                            </div> : <div></div> 
                        }
                    </Modal.Body>
                    <Modal.Footer>
                        <button type="button" className="btn btn-primary pull-right" onClick={() => this.downloadPDF()}>
                            Download
                        </button>
                    </Modal.Footer>
                </Modal>
            </span>

        )
    }

}
export default CoverLetter;