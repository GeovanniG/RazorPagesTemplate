﻿namespace RazorPagesTemplate.Infrastructure;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
