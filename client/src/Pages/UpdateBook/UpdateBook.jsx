import React from 'react';
import { useEffect, useState } from 'react';
import{ useParams } from 'react-router-dom';
import BookForm from '../../Components/BookForm/BookForm';
import { useContext } from 'react';
import { ThemeContext } from '../../Context/ThemeProvider';


export default function UpdateBook() {
const [book, setBook] = useState();
const { id } = useParams();
const [isLoading, setIsLoading] = useState(true);
const { darkMode } = useContext(ThemeContext);

useEffect(() => {
  setIsLoading(true);
    fetch(`https://localhost:7276/get-book/${id}`, { credentials: 'include'})
    .then(res => res.json())
    .then(data => {
        console.log(data);
        setBook(data);
    })
    .catch(err=> console.log(err))
.finally(() => setIsLoading(false));

}, [id]);

const handleUpdateBook = async (updatedBook) => {
    try {
      setIsLoading(true);
      const response = await fetch(`https://localhost:7276/update-book/${id}`, {
        method: 'PUT',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(updatedBook),
      });

      if (response.ok) {
        const {responseData} = await response.json();
        console.log('Book updated', responseData);
        setIsLoading(false);
      } else {
        console.error('Error updating book:', response.statusText);
      }
    } catch (err) {
      console.error('Error updating book:', err);
    }
  };
if(isLoading){
  return(
    <div>Loading...</div>
  )
}
  return (
        <div>
          
          <BookForm book={book} onFormSubmit={handleUpdateBook} />
        </div>
      
  );
}