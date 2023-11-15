import React from 'react'
import { useParams } from 'react-router';
import './UserAllPosts.css';
import { useState, useEffect } from 'react';

export default function UserAllPosts() {
    const [isLoading, setIsLoading] = useState(true);
    const [posts, setPosts] = useState([]);
    const { id } = useParams();

    useEffect(() => {
        setIsLoading(true);
        fetch(`https://localhost:7276/allposts/${id}`, { credentials: 'include' })
            .then((res) => res.json())
            .then((data) => {
                console.log(data);
                setPosts(data);
                setIsLoading(false);
            })
            .catch((err) => console.error("Error fetching posts", err));
    },[id]);

    if(isLoading){
        return <div>Loading...</div>;
    }
  return (
    <div>UserAllPosts</div>
  )
}
