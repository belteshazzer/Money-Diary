INSERT INTO NotificationType (Title, Message, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt, IsDeleted, History)
VALUES 
('Expense Exceed Budgeted Balance', 'Your expenses have exceeded the budgeted balance.', 'system', GETDATE(), 'system', GETDATE(), 0, NULL),
('Expense reached Budgeted Balance', 'Your expenses have reached the budgeted balance.', 'system', GETDATE(), 'system', GETDATE(), 0, NULL);