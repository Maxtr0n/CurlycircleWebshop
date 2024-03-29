﻿using BLL.Dtos;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IColorService
    {
        Task<EntityCreatedViewModel> CreateColorAsync(ColorUpsertDto colorCreateDto);

        Task<ColorsViewModel> GetAllColorsAsync();

        Task<ColorViewModel> FindColorByIdAsync(int colorId);

        Task DeleteColorAsync(int colorId);
    }
}
