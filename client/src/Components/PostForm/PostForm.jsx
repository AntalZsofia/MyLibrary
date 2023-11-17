import React from 'react'
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export default function PostForm( {post, onFormSubmit}) {
    const [content, setContent] = useState(post ? post.content : '');
    const [discussionThread, setDiscussionThread] = useState(post ? post.discussionThread : '');
    const navigate = useNavigate();


const handleCancelClick = () => {
navigate('/forum');
};

const handleFormSubmit = async () => {
    try{
        const updatedPost = {
            content: content,
            discussionThread: discussionThread
        };
        onFormSubmit(updatedPost);
        navigate('/forum');
    }
    catch(err){
        console.error(err);
    };
}
  return (
    <div className='update-post-form-container'>
    <div className="update-post-form">
      <h1>{post ? 'Update Post' : 'Create Post'}</h1>
      <label htmlFor="discussionThread">Discussion Thread</label>
      <input
        type="text"
        id="discussionThread"
        name="discussionThread"
        value={discussionThread}
        onChange={(e) => setDiscussionThread(e.target.value)}
      />
      <label htmlFor="content">Content</label>
      <input
        type="text"
        id="content"
        name="content"
        value={content}
        onChange={(e) => setContent(e.target.value)}
      />
      <div className="form-actions">
        <button type="button" onClick={handleCancelClick}>
          Cancel
        </button>
        <button type="button" onClick={handleFormSubmit}>
          {post ? 'Update' : 'Create'}
        </button>
      </div>
    </div>
    </div>
  )
}

