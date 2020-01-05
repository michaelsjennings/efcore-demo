USE EFCoreDemo
GO


SET XACT_ABORT ON
GO


BEGIN TRANSACTION
GO


DELETE FROM dbo.ReportSections
DELETE FROM dbo.Reports
DELETE FROM dbo.Events
DELETE FROM dbo.Locations
GO


-- Locations
SET IDENTITY_INSERT dbo.Locations ON
INSERT INTO dbo.Locations (Id, Description)
VALUES
	(1, '7408 Marvon Drive Dothan, AL 36301'),
	(2, '460 Winchester St. Norwood, MA 02062'),
	(3, '573 S. Railroad St. East Meadow, NY 11554'),
	(4, '65 Sherwood St. Manassas, VA 20109'),
	(5, '97 Paris Hill Avenue New Orleans, LA 70115'),
	(6, '231 Henry St. San Pablo, CA 94806')
SET IDENTITY_INSERT dbo.Locations OFF
DBCC CHECKIDENT('dbo.Locations', RESEED, 6)


-- Events
SET IDENTITY_INSERT dbo.Events ON
INSERT INTO dbo.Events (Id, Title, Date, LocationId)
VALUES
	(1, 'Lorem Ipsum Dolor Sit Amet, Consectetur Adipiscing Elit', DATEADD(DAY, -33, GETDATE()), 2),
	(2, 'Sed Do Eiusmod Tempor Incididunt Ut Labore Et Dolore Magna Aliqua', DATEADD(DAY, -22, GETDATE()), 4),
	(3, 'Ut Enim Ad Minim Veniam', DATEADD(DAY, -11, GETDATE()), 6)
SET IDENTITY_INSERT dbo.Events OFF
DBCC CHECKIDENT('dbo.Events', RESEED, 3)


-- Reports
SET IDENTITY_INSERT dbo.Reports ON
INSERT INTO dbo.Reports (Id, EventId, Title)
VALUES
	(1, 1, 'Quis Nostrud Exercitation Ullamco Laboris Nisi Ut Aliquip Ex Ea Commodo Consequat'),
	(2, 1, 'Duis Aute Irure Dolor in Reprehenderit in Voluptate Velit Esse Cillum'),
	(3, 2, 'Dolore Eu Fugiat Nulla Pariatur'),
	(4, 2, 'Excepteur Sint Occaecat Cupidatat Non Proident'),
	(5, 2, 'Sunt in Culpa Qui Officia Deserunt Mollit Anim Id Est Laborum'),
	(6, 3, 'Sed Ut Perspiciatis Unde Omnis Iste Natus Error')
SET IDENTITY_INSERT dbo.Reports OFF
DBCC CHECKIDENT('dbo.Reports', RESEED, 6)


-- ReportSections
SET IDENTITY_INSERT dbo.ReportSections ON
INSERT INTO dbo.ReportSections (Id, ReportId, Title, Body)
VALUES
	(1, 1, 'Sit Voluptatem Accusantium Doloremque Laudantium', 'The robot clicked disapprovingly, gurgled briefly inside its cubical interior and extruded a pony glass of brownish liquid.", Sir, you will undoubtedly end up in a drunkard''s grave, dead of hepatic cirrhosis," it informed me virtuously as it returned my ID card. I glared as I pushed the glass across the table.'),
	(2, 1, 'Totam Rem Aperiam', 'Was it enough? That was the question he kept asking himself. Was being satisfied enough? He looked around him at everyone yearning to just be satisfied in their daily life and he had reached that goal. He knew that he was satisfied and he also knew it wasn''t going to be enough.'),
	(3, 2, 'Eaque Ipsa Quae Ab Illo Inventore Veritatis Et Quasi Architecto Beatae Vitae Dicta Sunt Explicabo', 'It was going to rain. The weather forecast didn''t say that, but the steel plate in his hip did. He had learned over the years to trust his hip over the weatherman. It was going to rain, so he better get outside and prepare.'),
	(4, 3, 'Nemo Enim Ipsam Voluptatem Quia Voluptas Sit Aspernatur Aut Odit Aut Fugit', 'There wasn''t a bird in the sky, but that was not what caught her attention. It was the clouds. The deep green that isn''t the color of clouds, but came with these. She knew what was coming and she hoped she was prepared.'),
	(5, 3, 'Sed Quia Consequuntur Magni Dolores Eos Qui Ratione Voluptatem Sequi Nesciunt', 'Stranded. Yes, she was now the first person ever to land on Venus, but that was of little consequence. Her name would be read by millions in school as the first to land here, but that celebrity would never actually be seen by her. She looked at the control panel and knew there was nothing that would ever get it back into working order. She was the first and it was not clear this would also be her last.'),
	(6, 3, 'Neque Porro Quisquam Est', 'You can decide what you want to do in life, but I suggest doing something that creates. Something that leaves a tangible thing once you''re done. That way even after you''re gone, you will still live on in the things you created.'),
	(7, 4, 'Qui Dolorem Ipsum Quia Dolor Sit Amet', 'What was beyond the bend in the stream was unknown. Both were curious, but only one was brave enough to want to explore. That was the problem. There was always one that let fear rule her life.'),
	(8, 5, 'Sed Quia Non Numquam Eius Modi Tempora Incidunt', 'What were they eating? It didn''t taste like anything she had ever eaten before and although she was famished, she didn''t dare ask. She knew the answer would be one she didn''t want to hear.'),
	(9, 5, 'Ut Labore Et Dolore Magnam Aliquam Quaerat Voluptatem', 'A long black shadow slid across the pavement near their feet and the five Venusians, very much startled, looked overhead. They were barely in time to see the huge gray form of the carnivore before it vanished behind a sign atop a nearby building which bore the mystifying information "Pepsi-Cola."'),
	(10, 6, 'Ut Enim Ad Minima Veniam', 'Do you really listen when you are talking with someone? I have a friend who listens in an unforgiving way. She actually takes every word you say as being something important and when you have a friend that listens like that, words take on a whole new meaning.'),
	(11, 6, 'Quis Nostrum Exercitationem Ullam Corporis Suscipit Laboriosam', 'If you can imagine a furry humanoid seven feet tall, with the face of an intelligent gorilla and the braincase of a man, you''ll have a rough idea of what they looked like -- except for their teeth. The canines would have fitted better in the face of a tiger, and showed at the corners of their wide, thin-lipped mouths, giving them an expression of ferocity.'),
	(12, 6, 'Nisi Ut Aliquid Ex Ea Commodi Consequatur', 'Cake or pie? I can tell a lot about you by which one you pick. It may seem silly, but cake people and pie people are really different. I know which one I hope you are, but that''s not for me to decide. So, what is it? Cake or pie?')
SET IDENTITY_INSERT dbo.ReportSections OFF
DBCC CHECKIDENT('dbo.ReportSections', RESEED, 12)

COMMIT
GO
