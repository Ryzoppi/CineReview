using AutoMapper;
using CineReview.DTOs;
using CineReview.Models;
using CineReview.Repositories.Interfaces;
using CineReview.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CineReview.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return null;
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> CreateAsync(UserCreateDto dto)
        {
            var user = _mapper.Map<User>(dto);
            await _repo.AddAsync(user);
            if (!await _repo.SaveChangesAsync()) return null;
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> UpdateAsync(int id, UserCreateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            _repo.Update(existing);
            if (!await _repo.SaveChangesAsync()) return null;
            return _mapper.Map<UserReadDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            _repo.Remove(existing);
            return await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<MediaReadDto>> GetAllFavoritesAsync(int id)
        {
            var favorites = await _repo.GetAllFavoritesAsync(id);
            return _mapper.Map<IEnumerable<MediaReadDto>>(favorites);
        }

        public async Task<MediaReadDto> AddToFavoritesAsync(int id, int mediaId)
        {
            var media = await _repo.AddToFavoritesAsync(id, mediaId);
            if (media == null) return null;

            return _mapper.Map<MediaReadDto>(media);
        }

        public async Task<MediaReadDto> RemoveFromFavoritesAsync(int id, int mediaId)
        {
            var media = await _repo.RemoveFromFavoritesAsync(id, mediaId);
            if (media == null) return null;

            return _mapper.Map<MediaReadDto>(media);
        }
    }
}
