import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import PostForm from '../../Components/PostForm/PostForm';

export default function UpdatePost() {

  const { id } = useParams();
  const [post, setPost] = useState('');
  const [isLoading, setIsLoading] = useState(true);

  const getPost = () => {
    setIsLoading(true);
    fetch(`https://localhost:7276/get-post/${id}`, { credentials: 'include' })
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setPost(data);
        setIsLoading(false);
      })
      .catch((err) => console.error("Error fetching post", err));
  };

  useEffect(() => {
    getPost();
  }, []);

  const handleUpdatePost = async (updatedPost) => {
    try {
      setIsLoading(true);
      const response = await fetch(`https://localhost:7276/update-post/${id}`, {
        method: 'PUT',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(updatedPost),
      }); if (response.ok) {
        const { responseData } = await response.json();
        console.log('Post updated', responseData);
        getPost();
        setIsLoading(false);
      } else {
        console.error('Error updating post:', response.statusText);
      }
    } catch (err) {
      console.error('Error updating post:', err);
    }
  };
  if(isLoading){
    return <div>Loading...</div>
  };

  return (
    <div>

      <PostForm post={post} onFormSubmit={handleUpdatePost} />
    </div>
  )
}
