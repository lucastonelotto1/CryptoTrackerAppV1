﻿using CryptoTrackerApp.DataAccessLayer;
using CryptoTrackerApp.Domain;

public class UserRepository : IUserRepository
{
    private readonly Supabase.Client _supabaseClient;

    public UserRepository(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<User> GetUserById(string userId)
    {
        var response = await _supabaseClient
            .From<User>()
            .Where(x => x.Id == userId)
            .Single();

        return response;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var response = await _supabaseClient
            .From<User>()
            .Where(x => x.Email == email)
            .Single();

        return response;
    }

    public async Task AddUser(User user)
    {
        await _supabaseClient
            .From<User>()
            .Insert(user);
    }

    public async Task UpdateUser(User user)
    {
        await _supabaseClient
            .From<User>()
            .Update(user);
    }

    Task<User> IUserRepository.GetUserById(string userId)
    {
        throw new NotImplementedException();
    }

    Task<User> IUserRepository.GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

}

