import React, {useEffect, useState} from 'react';
import axios from 'axios';
import {
    FormControl,
    InputGroup,
    Button,
    Row,
    Col,
    Container,
    FloatingLabel,
    Form
} from 'react-bootstrap';

const Admin = () => {

    const [storePost,
        setStorePost] = useState([
        {
            postId: 0,
            content: "",
            title: "",
            author: ""
        }
    ]);
    const [content,
        setContent] = useState("");
    const [title,
        setTitle] = useState("");
    const [author,
        setAuthor] = useState("");

    const handleSubmit = (e) => {
        e.preventDefault();
        const newPost = {
            postId: Date.now(),
            content,
            title,
            author
        };
        axios
            .post("http://localhost:7244/api/Post/", newPost)
            .then((res) => {
                setStorePost([
                    ...content,
                    newPost
                ])
                setStorePost([
                    ...title,
                    newPost
                ])
                setStorePost([
                    ...author,
                    newPost
                ])
            })
            .catch((err) => {
                console.log(err);
            });
    };

    return (
        <div>

            <Container>

                <Row className="justify-content-md-center">
                    <Col className="mt-5" lg={10}>
                        <InputGroup className="mb-3">
                            <InputGroup.Text>Tytuł i autor</InputGroup.Text>
                            <FormControl
                                value={storePost.title}
                                aria-label="Title"
                                onChange={(e) => setTitle(e.target.value)}/>
                            <FormControl
                                value={storePost.author}
                                aria-label="Autor"
                                onChange={(e) => setAuthor(e.target.value)}/>
                        </InputGroup>
                        <FloatingLabel
                            controlId="floatingTextarea2"
                            label="Post"
                            className="text-break"
                            onChange={(e) => setContent(e.target.value)}>

                            <Form.Control type="text" value={storePost.content}/>
                            <div className="d-grid d-md-flex justify-content-md-end">

                                <Button
                                    className="mt-3 me-md-2"
                                    onClick={handleSubmit}
                                    variant="outline-secondary"
                                    id="button-addon2">Submit</Button>
                            </div>

                        </FloatingLabel>
                    </Col>
                </Row>
            </Container>

        </div>

    );
};

export default Admin;
