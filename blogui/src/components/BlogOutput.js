import {Button, Container} from 'react-bootstrap';
import {useState, useEffect} from 'react';
import axios from 'axios';

const BlogOutput = () => {
    const [visible,
        setVisible] = useState(false);

    const [storePost,
        setStorePost] = useState([]);

    const [comment,
        setComment] = useState([]);

    useEffect(() => {
        const url = 'http://localhost:7244/api/Post'
        axios
            .get(url)
            .then(res => {
                console.log(res.data);
                setStorePost(res.data);
                res?.data
                        .forEach(post => {
                            getComments(post.postId)
                        })

            })
            .catch(err => {
                console.log(err)
            })
    }, [])

    const getComments = (id) => {
        const url = `http://localhost:7244/api/Post/${id}/comments`
        axios
            .get(url)
            .then(res => {
                console.log(res.data);
                setComment(prev => [
                    ...prev,
                    ...res.data
                ])
            })
            .catch(err => {

                throw err;
            })
    }

    const handleDelete = (id) => {
        axios
            .delete(`http://localhost:7244/api/Post/${id}`)
            .then(res => {
                setStorePost(storePost.filter((item) => item.postId !== id))
                setComment(prev => prev.filter((comment) => comment.postId !== id))
            })
            .catch(err => {
                console.log(err);
            })
    }

    function convert(str) {

        const d = new Date(str);
        let month = '' + (d.getMonth() + 1);
        let day = '' + d.getDate();
        let year = d.getFullYear();
        if (month.length < 2) 
            month = '0' + month;
        if (day.length < 2) 
            day = '0' + day;
        let hour = '' + d.getUTCHours();
        let minutes = '' + d.getUTCMinutes();
        let seconds = '' + d.getUTCSeconds();
        if (hour.length < 2) 
            hour = '0' + hour;
        if (minutes.length < 2) 
            minutes = '0' + minutes;
        if (seconds.length < 2) 
            seconds = '0' + seconds;
        return [year, month, day].join('-') + ' ' + [hour, minutes, seconds].join(':');
    };

    const renderPosts = () => {
        return storePost.map((post) => {
            return (
                <div className="text-break" key={post.postId}>
                    <h3 className='d-flex justify-content-center mt-5'>{post.title}</h3>
                    <hr></hr>
                    <div className="mt-5 d-flex flex-row">
                        <p className="p-2">{convert(post.dateTime)}</p>
                        <p className="p-2 pe-1">Autor: {post.author}</p>
                    </div>
                    <p>{post.content}</p>

                    <div className="d-grid d-md-flex justify-content-md-end">
                        <Button
                            className="mt-3 me-md-2"
                            onClick={() => handleDelete(post.postId)}
                            variant="secondary">Delete</Button>
                    </div>
                    <Button
                        className="mt-3 me-md-2"
                        onClick={() => setVisible(true)}
                        variant="secondary">Pokaż komentarze</Button>
                    <Button
                        className="mt-3 me-md-2"
                        onClick={() => setVisible(false)}
                        variant="secondary">Schowaj koemntarze</Button>
                    {visible && <div>

                        {comment
                            .filter(comment => comment.postId === post.postId)
                            .map(comment => {
                                return (
                                    <p key={comment.commentId}>{comment.commented}
                                    </p>
                                )
                            })
}
                    </div>
}
                </div>
            )
        })
    }

    return (

        <Container fluid="md">
            {renderPosts()}
        </Container>
    );
};

export default BlogOutput;