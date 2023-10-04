
import React from 'react';
import {useEffect, useState } from 'react';

const MyBooks = () => {
    const [userBooks, setUserBooks ] = useState ([]);

    useEffect(() => {
fetch('http://localhost:5000/book/allbooks')
.then((res) => res.json())
.then((data) => {
    setUserBooks(data);
})
.catch((err) => console.error("Error fetching user books", err));
    },[]);

    return <>
    <ul>
        {userBooks.map((book) => (
            <li key={book.id}>{book.title}</li>
        ))}
    </ul>
    </>
}
export default MyBooks;