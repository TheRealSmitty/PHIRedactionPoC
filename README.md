# PHIRedactionPoC

Simple String Parser

Nothing special is required to run the application.

Select the file(s) you want to parse on the index web page. Then press the 'Get Redacted Data' button.

This will store the original file(s) and the sanitized file(s) in the application's "Data" and "Sanitized" directories.

This code was developed primarily on the knowledge of the supplied test data file. It considers that each line with PHI data contains ":". The method of recognition of PHI data is constructed via a list of keywords to identify information that should be redacted.

I might consider finding a library that handles this, but I believe this exercise was primarily focused on my ability to write c# code.
