import React, { useEffect, useState } from 'react'

export default function Admin() {
    const [isLoading, setIsLoading] = useState(true);
    const [users, setUsers] = useState([]);

useEffect(() => {
    setIsLoading(true);
    fetch('https://localhost:7276/users', {credentials: 'include'})
    .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setUsers(data);
        
        setIsLoading(false);
      })
      .catch((err) => console.error("Error fetching users", err));
  }, [])
if(isLoading){
    return <div>Loading...</div>
}

  return (
    <div>
      <h2>User Table</h2>
      <table>
        <thead>
          <tr>
            <th>Email</th>
            <th>Username</th>
            <th>Date of Profile Creation</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr key={user.id}>
              <td>{user.email}</td>
              <td>{user.username}</td>
              <td>{user.profileCreationDate}</td>
             
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
  
}
