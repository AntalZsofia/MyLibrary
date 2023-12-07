import React from 'react';
import { useEffect, useState } from 'react';
import{ useParams } from 'react-router-dom';
import Modal from './../../Components/Modal/Modal'
import { ThemeContext } from '../../Context/ThemeProvider';
import { useContext } from 'react';

export default function DeleteBook() {
    const [isDeleteConfirmationVisible, setDeleteConfirmationVisible] = useState(false);
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
      .finally(() => {
        setIsLoading(false)
        setDeleteConfirmationVisible(true);
    });
      
      }, [id]);

    const handleDeleteBook = async () => {
        try {
          const bookToDelete = {
            title: book.title,
            author: book.author,
            genre: book.genre,
            publishYear: book.publishYear,
            description: book.description,
            imageUrl: book.imageUrl,
          };
          const response = await fetch(`https://localhost:7276/delete-book/${id}`, {
            method: 'DELETE',
            credentials: 'include',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(bookToDelete),
          });
          if (response.ok) {
            const responseData = await response.json();
            setDeleteConfirmationVisible(false);
           
            console.log('Book deleted from the collection:', responseData);
    
          } else {
            console.error('Error deleting book from collection:', response.statusText);
          }
        } catch (err) {
          console.error('Error deleting book from collection', err);
        }
      };
      const openDeleteConfirmation = () => {
        setDeleteConfirmationVisible(true);
      };
    
      const closeDeleteConfirmation = () => {
        setDeleteConfirmationVisible(false);
      };
      if(isLoading){
        return <div>Loading...</div>
      }

  return (
    <>
    <button className="delete-button" onClick={openDeleteConfirmation}>
  Delete Book
</button>
  
    <div>
        {isDeleteConfirmationVisible && (
            <Modal onClose={closeDeleteConfirmation}>
            <h3>Are you sure you want to delete this book?</h3>
            <button className={`yesButton ${darkMode ? 'dark-mode' : ''}`} onClick={handleDeleteBook}>Yes</button>
            <button className={`noButton ${darkMode ? 'dark-mode' : ''}`} onClick={closeDeleteConfirmation}>No</button>
          </Modal>
          )}

    </div>
    </>
  )
}
