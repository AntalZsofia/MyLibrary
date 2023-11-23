import React, { useEffect, useState } from 'react'
import './AllReviews.css';

export default function AllReviews() {
    const [reviews, setReviews] = useState([]);
   


    const getReviews = async () => {
        try {
            const response = await fetch('https://localhost:7276/reviews', { credentials: 'include' });
    
            if (!response.ok) {
                console.error('Error fetching reviews', response.statusText);
                return;
            }
    
            const data = await response.json();
            console.log(data);
            
            setReviews(data);
        } catch (err) {
            console.error("Error fetching reviews", err);
        }
    }

    useEffect(() => {
        getReviews();
    }, [])

    return (

        <div className='all-reviews-container'>
            {!reviews || reviews.length === 0 ? (
                <div className='no-reviews-message'>There are no reviews to show.</div>
            ) : (
                <div className="reviews-list">
                    {reviews.map((review, index) => (
                        <div className='review-card'key={review.id || index}>
                            <img src={review.smallCoverImage} alt={review.title} className='review-card-image' />
                            <div className='review-card-header'>
                                <div className='review-card-title'>{review.title}</div>
                                <div className='review-card-author'>by {review.author}</div>
                            </div>
                            <div className='review-card-rating'>Rating: {review.rating}</div>
                            <div className='review-card-review'>Review: {review.review}</div>
                            <div className='review-buttons-container'>
                                <button className='review-card-edit-button'>Edit</button>
                                <button className='review-card-delete-button'>Delete</button>
                            </div>
                        </div>
                    ))}
                    /</div>)
            } </div>)
}





